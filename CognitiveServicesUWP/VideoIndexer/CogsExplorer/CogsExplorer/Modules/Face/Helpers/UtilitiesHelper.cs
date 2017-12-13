using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face.Helpers
{
    public static class UtilitiesHelper
    {
        public async static Task<bool> ClearAllFaceInformationAsync()
        {
            bool successful = false;

            //REMOVE ALL FACE LISTS
            var faceLists = await RecognitionHelper.GetFaceListsAsync();

            foreach (var faceList in faceLists)
            {
                await RecognitionHelper.DeleteFaceListAsync(faceList.faceListId);
            }

            //REMOVE ALL PEOPLE GROUPS
            var personGroups = await RecognitionHelper.GetPersonGroupsAsync();

            foreach (var personGroup in personGroups)
            {
                await RecognitionHelper.DeletePersonGroupAsync(personGroup.personGroupId);
            }

            return successful;
        }
    }
}
