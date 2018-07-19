using System.Collections.Generic;
using Xunit;

namespace Api.Mailgun.Tests
{
    public class MailingLists
    {
        [Fact]
        public async void Test()
        {
            var mg = new Mailgun(Settings.WorkDomain, Settings.ApiKey);

            #region create lists
            var createList1 = await mg.CreateMailingListAsync("test1");
            Assert.True(createList1.Successful, createList1.ErrorMessage);
            Assert.Equal($"test1@{Settings.WorkDomain}", createList1.Response.MailingList.Address);
            Assert.Equal("", createList1.Response.MailingList.Name);
            Assert.Equal("", createList1.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, createList1.Response.MailingList.AccessLevel);

            var createList2 = await mg.CreateMailingListAsync("test2", "Test list 2");
            Assert.True(createList2.Successful, createList2.ErrorMessage);
            Assert.Equal($"test2@{Settings.WorkDomain}", createList2.Response.MailingList.Address);
            Assert.Equal("Test list 2", createList2.Response.MailingList.Name);
            Assert.Equal("", createList2.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, createList2.Response.MailingList.AccessLevel);

            var createList3 = await mg.CreateMailingListAsync("test3", "Test list 3", "Description of the list 3");
            Assert.True(createList3.Successful, createList3.ErrorMessage);
            Assert.Equal($"test3@{Settings.WorkDomain}", createList3.Response.MailingList.Address);
            Assert.Equal("Test list 3", createList3.Response.MailingList.Name);
            Assert.Equal("Description of the list 3", createList3.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, createList3.Response.MailingList.AccessLevel);

            var createList4 = await mg.CreateMailingListAsync("test4", "Test list 4", "Description of the list 4", AccessLevels.Members);
            Assert.True(createList4.Successful, createList4.ErrorMessage);
            Assert.Equal($"test4@{Settings.WorkDomain}", createList4.Response.MailingList.Address);
            Assert.Equal("Test list 4", createList4.Response.MailingList.Name);
            Assert.Equal("Description of the list 4", createList4.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Members, createList4.Response.MailingList.AccessLevel);
            #endregion

            #region update lists
            var updateList1 = await mg.UpdateMailingListAliasAsync("test1", "test1_new");
            Assert.True(updateList1.Successful, updateList1.ErrorMessage);
            Assert.Equal($"test1_new@{Settings.WorkDomain}", updateList1.Response.MailingList.Address);
            Assert.Equal("", updateList1.Response.MailingList.Name);
            Assert.Equal("", updateList1.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, updateList1.Response.MailingList.AccessLevel);

            updateList1 = await mg.UpdateMailingListNameAsync("test1_new", "New name");
            Assert.True(updateList1.Successful, updateList1.ErrorMessage);
            Assert.Equal($"test1_new@{Settings.WorkDomain}", updateList1.Response.MailingList.Address);
            Assert.Equal("New name", updateList1.Response.MailingList.Name);
            Assert.Equal("", updateList1.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, updateList1.Response.MailingList.AccessLevel);

            updateList1 = await mg.UpdateMailingListDescriptionAsync("test1_new", "New description");
            Assert.True(updateList1.Successful, updateList1.ErrorMessage);
            Assert.Equal($"test1_new@{Settings.WorkDomain}", updateList1.Response.MailingList.Address);
            Assert.Equal("New name", updateList1.Response.MailingList.Name);
            Assert.Equal("New description", updateList1.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Readonly, updateList1.Response.MailingList.AccessLevel);

            updateList1 = await mg.UpdateMailingListAccessAsync("test1_new", AccessLevels.Everyone);
            Assert.True(updateList1.Successful, updateList1.ErrorMessage);
            Assert.Equal($"test1_new@{Settings.WorkDomain}", updateList1.Response.MailingList.Address);
            Assert.Equal("New name", updateList1.Response.MailingList.Name);
            Assert.Equal("New description", updateList1.Response.MailingList.Description);
            Assert.Equal(AccessLevels.Everyone, updateList1.Response.MailingList.AccessLevel);
            #endregion

            #region add members
            var addMember1 = await mg.AddMemberToListAsync("test1_new", "member1@test.com");
            Assert.True(addMember1.Successful, addMember1.ErrorMessage);
            Assert.Equal("member1@test.com", addMember1.Response.Member.Address);
            Assert.Equal("", addMember1.Response.Member.Name);
            Assert.True(addMember1.Response.Member.Subscribed);
            Assert.Empty(addMember1.Response.Member.Vars);

            var addMember2 = await mg.AddMemberToListAsync("test1_new", "member2@test.com", "Member 2");
            Assert.True(addMember2.Successful, addMember2.ErrorMessage);
            Assert.Equal("member2@test.com", addMember2.Response.Member.Address);
            Assert.Equal("Member 2", addMember2.Response.Member.Name);
            Assert.True(addMember2.Response.Member.Subscribed);
            Assert.Empty(addMember2.Response.Member.Vars);

            var addMember3 = await mg.AddMemberToListAsync("test1_new", "member3@test.com", "Member 3", new Dictionary<string, string>() { { "Age", "18" }, { "Gender", "Male" } });
            Assert.True(addMember3.Successful, addMember3.ErrorMessage);
            Assert.Equal("member3@test.com", addMember3.Response.Member.Address);
            Assert.Equal("Member 3", addMember3.Response.Member.Name);
            Assert.True(addMember3.Response.Member.Subscribed);
            Assert.Collection(addMember3.Response.Member.Vars,
                item =>
                {
                    Assert.Equal("Age", item.Key);
                    Assert.Equal("18", item.Value);
                },
                item =>
                {
                    Assert.Equal("Gender", item.Key);
                    Assert.Equal("Male", item.Value);
                });
            #endregion

            #region update members
            var updateMember1 = await mg.UpdateMemberAddressAsync("test1_new", "member1@test.com", "member1_new@test.com");
            Assert.True(updateMember1.Successful, updateMember1.ErrorMessage);
            Assert.Equal("member1_new@test.com", updateMember1.Response.Member.Address);
            Assert.Equal("", updateMember1.Response.Member.Name);
            Assert.True(updateMember1.Response.Member.Subscribed);
            Assert.Empty(updateMember1.Response.Member.Vars);

            updateMember1 = await mg.UpdateMemberNameAsync("test1_new", "member1_new@test.com", "Member 1 new name");
            Assert.True(updateMember1.Successful, updateMember1.ErrorMessage);
            Assert.Equal("member1_new@test.com", updateMember1.Response.Member.Address);
            Assert.Equal("Member 1 new name", updateMember1.Response.Member.Name);
            Assert.True(updateMember1.Response.Member.Subscribed);
            Assert.Empty(updateMember1.Response.Member.Vars);

            updateMember1 = await mg.UpdateMemberStatusAsync("test1_new", "member1_new@test.com", false);
            Assert.True(updateMember1.Successful, updateMember1.ErrorMessage);
            Assert.Equal("member1_new@test.com", updateMember1.Response.Member.Address);
            Assert.Equal("Member 1 new name", updateMember1.Response.Member.Name);
            Assert.False(updateMember1.Response.Member.Subscribed);
            Assert.Empty(updateMember1.Response.Member.Vars);

            updateMember1 = await mg.UpdateMemberVarsAsync("test1_new", "member1_new@test.com", new Dictionary<string, string>() { { "Weight", "100" } });
            Assert.True(updateMember1.Successful, updateMember1.ErrorMessage);
            Assert.Equal("member1_new@test.com", updateMember1.Response.Member.Address);
            Assert.Equal("Member 1 new name", updateMember1.Response.Member.Name);
            Assert.False(updateMember1.Response.Member.Subscribed);
            Assert.Collection(updateMember1.Response.Member.Vars,
                item =>
                {
                    Assert.Equal("Weight", item.Key);
                    Assert.Equal("100", item.Value);
                });

            updateMember1 = await mg.UpdateMemberVarsAsync("test1_new", "member1_new@test.com", new Dictionary<string, string>());
            Assert.True(updateMember1.Successful, updateMember1.ErrorMessage);
            Assert.Equal("member1_new@test.com", updateMember1.Response.Member.Address);
            Assert.Equal("Member 1 new name", updateMember1.Response.Member.Name);
            Assert.False(updateMember1.Response.Member.Subscribed);
            Assert.Empty(updateMember1.Response.Member.Vars);
            #endregion

            #region remove members
            var removeMember = await mg.RemoveMemberFromListAsync("test1_new", "member1_new@test.com");
            Assert.True(removeMember.Successful, removeMember.ErrorMessage);
            Assert.Equal("member1_new@test.com", removeMember.Response.Address);

            removeMember = await mg.RemoveMemberFromListAsync("test1_new", "member2@test.com");
            Assert.True(removeMember.Successful, removeMember.ErrorMessage);
            Assert.Equal("member2@test.com", removeMember.Response.Address);

            removeMember = await mg.RemoveMemberFromListAsync("test1_new", "member3@test.com");
            Assert.True(removeMember.Successful, removeMember.ErrorMessage);
            Assert.Equal("member3@test.com", removeMember.Response.Address);
            #endregion

            #region remove lists
            var removeList = await mg.DeleteMailingListAsync("test1_new");
            Assert.True(removeList.Successful, removeList.ErrorMessage);
            Assert.Equal($"test1_new@{Settings.WorkDomain}", removeList.Response.Address);

            removeList = await mg.DeleteMailingListAsync("test2");
            Assert.True(removeList.Successful, removeList.ErrorMessage);
            Assert.Equal($"test2@{Settings.WorkDomain}", removeList.Response.Address);

            removeList = await mg.DeleteMailingListAsync("test3");
            Assert.True(removeList.Successful, removeList.ErrorMessage);
            Assert.Equal($"test3@{Settings.WorkDomain}", removeList.Response.Address);

            removeList = await mg.DeleteMailingListAsync("test4");
            Assert.True(removeList.Successful, removeList.ErrorMessage);
            Assert.Equal($"test4@{Settings.WorkDomain}", removeList.Response.Address);
            #endregion
        }
    }
}
