class PollActivity < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :payload, default: -> { JSON.parse("{}") }

  validates :type, presence: true

  belongs_to :poll, inverse_of: :activities
end
