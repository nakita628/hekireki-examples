class LiteLLMLensTraceSignal < ApplicationRecord
  self.table_name = "LiteLLM_LensTraceSignal"
  self.primary_key = %w[trace_id trace_ref]

  attribute :trace_ref, default: ""

  validates :config_key, presence: true
  validates :span_count, presence: true
  validates :data, exclusion: { in: [ nil ] }
end
