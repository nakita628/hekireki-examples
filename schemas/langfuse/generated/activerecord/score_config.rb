class ScoreConfig < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_archived, default: false

  enum :data_type, { categorical: "CATEGORICAL", numeric: "NUMERIC", boolean: "BOOLEAN", text: "TEXT" }, validate: true

  validates :id, uniqueness: { scope: :project_id }
  validates :name, presence: true

  belongs_to :project, inverse_of: :score_config
end
