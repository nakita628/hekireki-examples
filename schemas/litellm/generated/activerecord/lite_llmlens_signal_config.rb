class LiteLLMLensSignalConfig < ApplicationRecord
  self.table_name = "LiteLLM_LensSignalConfig"

  validates :data, exclusion: { in: [ nil ] }
end
