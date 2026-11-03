class TempOrgRedirect < ApplicationRecord
  self.table_name = "TempOrgRedirect"
  self.inheritance_column = nil

  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { user_event_type: "user-event-type", team_event_type: "team-event-type", user: "user", team: "team" },
       validate: true

  validates :from, presence: true, uniqueness: { scope: [ :type, :fromOrgId ] }
  validates :fromOrgId, presence: true
  validates :toUrl, presence: true
end
