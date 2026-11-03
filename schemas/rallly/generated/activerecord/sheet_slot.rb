class SheetSlot < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :start_time, presence: true

  belongs_to :sheet, inverse_of: :slots
  belongs_to :event_type, inverse_of: :slots
  has_one :scheduled_event, dependent: :nullify
end
