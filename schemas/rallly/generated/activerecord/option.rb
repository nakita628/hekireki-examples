class Option < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :start_time, :datetime, precision: 3
  attribute :duration_minutes, default: 0

  validates :start_time, presence: true

  belongs_to :poll, inverse_of: :options
  has_many :votes, dependent: :destroy
end
