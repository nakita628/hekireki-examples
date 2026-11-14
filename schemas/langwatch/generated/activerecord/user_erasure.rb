class UserErasure < ApplicationRecord
  self.table_name = "UserErasure"

  attribute :id, default: -> { Nanoid.generate }

  validates :accountCreatedAt, presence: true
end
