class PosthogIntegration < ApplicationRecord
  self.primary_key = "project_id"

  attribute :backfill, default: true

  enum :export_source, { traces_observations: "TRACES_OBSERVATIONS", traces_observations_events: "TRACES_OBSERVATIONS_EVENTS", events: "EVENTS" },
       default: :traces_observations, validate: true

  validates :encrypted_posthog_api_key, presence: true
  validates :posthog_host_name, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }

  belongs_to :project, inverse_of: :posthog_integration
end
