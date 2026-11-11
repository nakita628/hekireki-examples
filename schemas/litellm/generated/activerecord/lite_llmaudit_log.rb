class LiteLLMAuditLog < ApplicationRecord
  self.table_name = "LiteLLM_AuditLog"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :changed_by, default: ""
  attribute :changed_by_api_key, default: ""

  validates :action, presence: true
  validates :table_name, presence: true
  validates :object_id, presence: true
end
