class SocialMediaAgency < ApplicationRecord
  self.table_name = "SocialMediaAgency"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :approved, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true
  validates :name, presence: true
  validates :shortDescription, presence: true
  validates :description, presence: true

  belongs_to :logo, class_name: "Media", foreign_key: "logoId", optional: true, inverse_of: :agencies
  belongs_to :user, foreign_key: "userId", inverse_of: :agencies
  has_many :niches, class_name: "SocialMediaAgencyNiche", foreign_key: "agencyId", inverse_of: :agency,
                    dependent: :restrict_with_error
end
