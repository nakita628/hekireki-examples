class LiteLLMLensIngestionKey < ApplicationRecord
  self.table_name = "LiteLLM_LensIngestionKey"

  validates :data, exclusion: { in: [ nil ] }
end
