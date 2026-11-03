class Model < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :model_name, presence: true,
                         uniqueness: { scope: %i[project_id start_date unit], conditions: -> { where.not(project_id: nil).where.not(start_date: nil).where.not(unit: nil) } }
  validates :match_pattern, presence: true

  belongs_to :project, optional: true, inverse_of: :model
  has_many :price, class_name: "Price", inverse_of: :model, dependent: :destroy
  has_many :pricing_tiers, dependent: :destroy
end
