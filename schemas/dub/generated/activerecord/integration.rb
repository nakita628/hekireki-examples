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

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :integrations
  belongs_to :project, foreign_key: "projectId", inverse_of: :integrations
  has_one :o_auth_app, foreign_key: "integrationId", inverse_of: :integration, dependent: :destroy
  has_many :installations, class_name: "InstalledIntegration", foreign_key: "integrationId", inverse_of: :integration,
                           dependent: :destroy
end
