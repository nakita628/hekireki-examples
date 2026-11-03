class Star < ApplicationRecord
  self.table_name = "Star"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :stars, presence: true
  validates :totalStars, presence: true
  validates :forks, presence: true
  validates :totalForks, presence: true
  validates :login, presence: true, uniqueness: { scope: :date }
end
