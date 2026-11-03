class BatchAction < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :user_id, presence: true
  validates :action_type, presence: true
  validates :table_name, presence: true
  validates :status, presence: true
  validates :query, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :batch_action
end
