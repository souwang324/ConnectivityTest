using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace ConnectivityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectivityTest1();
        }

        public static void ConnectivityTest1()
        {
            vtkSTLReader pSTLReader = vtkSTLReader.New();
            pSTLReader.SetFileName("../../../../res/cow.stl");
            pSTLReader.Update();

            vtkPolyDataConnectivityFilter conFilter = vtkPolyDataConnectivityFilter.New();
            conFilter.SetInputConnection(pSTLReader.GetOutputPort());
            // 1) all regions
            //conFilter.SetExtractionModeToAllRegions();

            // 2) largest Region
            conFilter.SetExtractionModeToLargestRegion();

            // 3) seed
            //conFilter.AddSeed(id);
            //conFilter.SetExtractionModeToCellSeededRegions();
            //conFilter.SetExtractionModeToPointSeededRegions();

            // 4) closest point
            //conFilter.SetClosestPoint(x, y, z);
            // conFilter.SetExtractionModeToClosestPointRegion();

            conFilter.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(conFilter.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();

        }
    }
}
