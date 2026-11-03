class DSyncTeamGroupMapping < ApplicationRecord
  self.table_name = "DSyncTeamGroupMapping"

  validates :organizationId, presence: true
  validates :groupName, presence: true, uniqueness: { scope: :teamId }

  belongs_to :team, foreign_key: "teamId", inverse_of: :dsync_team_group_mapping
  belongs_to :directory, class_name: "DSyncData", foreign_key: "directoryId", primary_key: "directoryId",
                         inverse_of: :team_group_mapping
end
