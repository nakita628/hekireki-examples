class LiteLLMTelemetryReport < ApplicationRecord
  self.table_name = "LiteLLM_TelemetryReport"

  validates :window_start, presence: true
  validates :window_end, presence: true
  validates :report, exclusion: { in: [ nil ] }
end
