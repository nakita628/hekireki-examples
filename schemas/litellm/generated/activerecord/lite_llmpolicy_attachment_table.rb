class LiteLLMPolicyAttachmentTable < ApplicationRecord
  self.table_name = "LiteLLM_PolicyAttachmentTable"
  self.primary_key = "attachment_id"
  self.implicit_order_column = "created_at"

  attribute :attachment_id, default: -> { SecureRandom.uuid }
  attribute :teams, default: -> { [] }
  attribute :keys, default: -> { [] }
  attribute :models, default: -> { [] }
  attribute :tags, default: -> { [] }
  attribute :is_default, default: false

  validates :policy_name, presence: true
end
