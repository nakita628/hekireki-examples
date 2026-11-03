class Media < ApplicationRecord
  self.table_name = "Media"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :fileSize, default: 0
  attribute :type, default: "image"
  attribute :status, default: "ready"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :path, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :media
  has_many :agencies, class_name: "SocialMediaAgency", foreign_key: "logoId", inverse_of: :logo, dependent: :nullify
  has_many :user_picture, class_name: "User", foreign_key: "pictureId", inverse_of: :picture, dependent: :nullify
  has_many :oauth_apps, class_name: "OAuthApp", foreign_key: "pictureId", inverse_of: :picture, dependent: :nullify
end
