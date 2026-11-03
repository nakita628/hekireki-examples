class SpaceApiKey < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true
  validates :prefix, presence: true, uniqueness: true
  validates :hashed_key, presence: true

  belongs_to :space, inverse_of: :api_keys
end
