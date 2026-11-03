class DatasetItemMedia < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :project_id, presence: true,
                         uniqueness: { scope: %i[dataset_item_id dataset_item_valid_from field json_path], conditions: -> { where.not(dataset_item_valid_from: nil).where.not(json_path: nil) } }
  validates :media_id, presence: true
  validates :dataset_id, presence: true
  validates :dataset_item_id, presence: true
  validates :field, presence: true
end
