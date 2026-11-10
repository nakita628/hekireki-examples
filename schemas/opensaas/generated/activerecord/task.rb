class Task < ApplicationRecord
  self.table_name = "Task"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :time, default: "1"
  attribute :isDone, default: false
  alias_attribute :created_at, :createdAt

  validates :description, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :tasks
end
