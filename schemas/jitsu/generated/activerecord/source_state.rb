class SourceState < ApplicationRecord
  self.table_name = "source_state"
  self.primary_key = %w[sync_id stream]

  validates :state, exclusion: { in: [ nil ] }
end
