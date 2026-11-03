class TeamGroup < ApplicationRecord
  self.table_name = "TeamGroup"

  enum :teamRole, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER" }, validate: true

  validates :teamId, uniqueness: { scope: :organisationGroupId }

  belongs_to :organisation_group, foreign_key: "organisationGroupId", inverse_of: :team_groups
  belongs_to :team, foreign_key: "teamId", inverse_of: :team_groups
end
