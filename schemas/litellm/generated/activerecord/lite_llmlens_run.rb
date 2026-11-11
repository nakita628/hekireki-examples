class LiteLLMLensRun < ApplicationRecord
  self.table_name = "LiteLLM_LensRun"

  validates :lens_id, presence: true
  validates :data, exclusion: { in: [ nil ] }
end
