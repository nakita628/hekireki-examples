class LiteLLMManagedFileContentTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedFileContentTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :content, exclusion: { in: [ nil ] }
end
