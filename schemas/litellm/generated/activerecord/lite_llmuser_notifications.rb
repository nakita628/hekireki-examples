class LiteLLMUserNotifications < ApplicationRecord
  self.table_name = "LiteLLM_UserNotifications"
  self.primary_key = "request_id"

  attribute :models, default: -> { [] }

  validates :user_id, presence: true
  validates :justification, presence: true
  validates :status, presence: true
end
