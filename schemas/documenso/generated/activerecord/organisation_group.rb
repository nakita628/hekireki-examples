class OrganisationGroup < ApplicationRecord
  self.table_name = "OrganisationGroup"
  self.inheritance_column = nil

  enum :type, { internal_organisation: "INTERNAL_ORGANISATION", internal_team: "INTERNAL_TEAM", custom: "CUSTOM" },
       validate: true
  enum :organisationRole, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER" }, validate: true

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :groups
  has_many :organisation_group_members, foreign_key: "groupId", inverse_of: :group, dependent: :destroy
  has_many :team_groups, foreign_key: "organisationGroupId", inverse_of: :organisation_group, dependent: :destroy
end
