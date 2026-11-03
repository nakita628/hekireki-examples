class OrganisationGroupMember < ApplicationRecord
  self.table_name = "OrganisationGroupMember"

  validates :organisationMemberId, uniqueness: { scope: :groupId }

  belongs_to :group, class_name: "OrganisationGroup", foreign_key: "groupId", inverse_of: :organisation_group_members
  belongs_to :organisation_member, foreign_key: "organisationMemberId", inverse_of: :organisation_group_members
end
