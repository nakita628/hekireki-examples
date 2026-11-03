class AvatarImage < ApplicationRecord
  self.table_name = "AvatarImage"

  attribute :id, default: -> { Cuid.generate }

  validates :bytes, presence: true

  has_many :team, class_name: "Team", foreign_key: "avatarImageId", inverse_of: :avatar_image, dependent: :nullify
  has_many :user, class_name: "User", foreign_key: "avatarImageId", inverse_of: :avatar_image, dependent: :nullify
  has_many :organisation, class_name: "Organisation", foreign_key: "avatarImageId", inverse_of: :avatar_image,
                          dependent: :nullify
end
