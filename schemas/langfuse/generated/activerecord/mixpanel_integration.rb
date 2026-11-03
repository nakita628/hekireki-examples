class MixpanelIntegration < ApplicationRecord
  self.primary_key = "project_id"

  attribute :backfill, default: true

  enum :export_source, { traces_observations: "TRACES_OBSERVATIONS", traces_observations_events: "TRACES_OBSERVATIONS_EVENTS", events: "EVENTS" },
       default: :traces_observations, validate: true

  validates :encrypted_mixpanel_project_token, presence: true
  validates :mixpanel_region, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }

  belongs_to :project, inverse_of: :mixpanel_integration
end
