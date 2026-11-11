class LiteLLMManagedObjectTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedObjectTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :batch_processed, default: false
  attribute :request_tags, default: -> { JSON.parse("[]") }

  validates :unified_object_id, presence: true, uniqueness: true
  validates :model_object_id, presence: true, uniqueness: true
  validates :file_object, exclusion: { in: [ nil ] }
  validates :file_purpose, presence: true
end
