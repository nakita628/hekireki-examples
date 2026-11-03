class App < ApplicationRecord
  self.table_name = "App"
  self.primary_key = "slug"

  attribute :categories, default: -> { [] }
  attribute :enabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :dirName, presence: true, uniqueness: true

  has_many :credentials, foreign_key: "appId", inverse_of: :app, dependent: :destroy
  has_many :payments, foreign_key: "appId", inverse_of: :app, dependent: :destroy
  has_many :webhook, foreign_key: "appId", inverse_of: :app, dependent: :destroy
  has_many :api_key, foreign_key: "appId", inverse_of: :app, dependent: :destroy
end
