class AiScheduledTask < ApplicationRecord
  self.table_name = "aiScheduledTask"

  attribute :isEnabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, length: { maximum: 255 }
  validates :prompt, presence: true
  validates :schedule, presence: true, length: { maximum: 100 }

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", inverse_of: :ai_scheduled_tasks
end
