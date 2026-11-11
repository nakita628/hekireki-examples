class LiteLLMLensDataset < ApplicationRecord
  self.table_name = "LiteLLM_LensDataset"
  self.primary_key = %w[id revision]

  validates :data, exclusion: { in: [ nil ] }
end
