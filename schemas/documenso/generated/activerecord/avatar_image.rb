class AvatarImage < ApplicationRecord
  self.table_name = "AvatarImage"

  attribute :id, default: -> { Cuid.generate }

  validates :bytes, presence: true

  has_many :team, foreign_key: "avatarImageId", inverse_of: :avatar_image, dependent: :nullify
  has_many :user, foreign_key: "avatarImageId", inverse_of: :avatar_image, dependent: :nullify
  has_many :organisation, foreign_key: "avatarImageId", inverse_of: :avatar_image, dependent: :nullify
end
