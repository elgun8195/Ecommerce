using AzureProject.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Business.Abstract
{
    public interface ISliderService
    {
        List<Slider> GetList();
        Slider GetByIdSlider(int id); // Dal dan gelen T nesnesini karsilar
        void SliderAdd(Slider slider);
        void SliderDelete(Slider slider);
        void SliderUpdate(Slider slider);
    }
}
