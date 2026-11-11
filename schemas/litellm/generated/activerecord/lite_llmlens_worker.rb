class LiteLLMLensWorker < ApplicationRecord
  self.table_name = "LiteLLM_LensWorker"

  validates :token_hash, presence: true, uniqueness: true
  validates :data, exclusion: { in: [ nil ] }
end
