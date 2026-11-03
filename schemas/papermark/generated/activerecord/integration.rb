class Integration < ApplicationRecord
  self.table_name = "Integration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: false
  attribute :comingSoon, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true
  validates :developer, presence: true
  validates :website, presence: true

  has_many :installed_integrations, foreign_key: "integrationId", inverse_of: :integration, dependent: :destroy
end
