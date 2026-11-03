class TeamGlobalSettings < ApplicationRecord
  self.table_name = "TeamGlobalSettings"

  enum :documentVisibility, { everyone: "EVERYONE", manager_and_above: "MANAGER_AND_ABOVE", admin: "ADMIN" },
       validate: { allow_nil: true }

  belongs_to :email, class_name: "OrganisationEmail", foreign_key: "emailId", optional: true,
                     inverse_of: :team_global_settings
  has_one :team, foreign_key: "teamGlobalSettingsId", inverse_of: :team_global_settings, dependent: :destroy
end
