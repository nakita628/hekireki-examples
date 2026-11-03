class Domain < ApplicationRecord
  self.table_name = "Domain"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: false
  attribute :isDefault, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :slug, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :domains
  belongs_to :team, foreign_key: "teamId", inverse_of: :domains
  has_many :links, foreign_key: "domainId", inverse_of: :domain, dependent: :nullify
end
