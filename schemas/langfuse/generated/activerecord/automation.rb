class Automation < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true

  belongs_to :trigger, inverse_of: :automations
  belongs_to :action, inverse_of: :automations
  belongs_to :project, inverse_of: :automation
  has_many :automation_execution, dependent: :destroy
end
