class DatasetItem < ApplicationRecord
  self.primary_key = %w[id project_id valid_from]

  attribute :id, default: -> { Cuid.generate }
  attribute :is_deleted, default: false

  enum :status, { active: "ACTIVE", archived: "ARCHIVED" }, default: :active, validate: { allow_nil: true }

  belongs_to :dataset, foreign_key: %i[dataset_id project_id], inverse_of: :dataset_items
end
