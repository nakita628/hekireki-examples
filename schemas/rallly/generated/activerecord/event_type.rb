class EventType < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false

  validates :name, presence: true
  validates :duration_minutes, presence: true

  belongs_to :space, inverse_of: :event_types
  belongs_to :host, class_name: "User", inverse_of: :hosted_event_types
  has_many :slots, class_name: "SheetSlot", inverse_of: :event_type, dependent: :restrict_with_error
  has_many :scheduled_events, dependent: :nullify
end
