class Trigger < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :eventActions, default: -> { [] }

  enum :status, { active: "ACTIVE", inactive: "INACTIVE" }, default: :active, validate: true

  validates :eventSource, presence: true

  belongs_to :project, inverse_of: :triggers
  has_many :automation_executions, dependent: :destroy
  has_many :automations, dependent: :destroy
end
