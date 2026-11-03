class TableViewPreset < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true
  validates :table_name, presence: true, uniqueness: { scope: %i[project_id name] }
  validates :filters, exclusion: { in: [ nil ] }
  validates :column_order, exclusion: { in: [ nil ] }
  validates :column_visibility, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :table_view_preset
  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :table_view_preset_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :table_view_preset_updated
end
