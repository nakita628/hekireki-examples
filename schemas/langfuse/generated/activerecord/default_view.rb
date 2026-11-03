class DefaultView < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :view_name, presence: true
  validates :view_id, presence: true

  belongs_to :project, inverse_of: :default_view
  belongs_to :user, optional: true, inverse_of: :default_views
end
