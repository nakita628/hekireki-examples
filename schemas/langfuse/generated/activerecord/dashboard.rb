class Dashboard < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :filters, default: -> { JSON.parse("[]") }

  validates :name, presence: true
  validates :description, presence: true
  validates :definition, exclusion: { in: [ nil ] }

  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :dashboard_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :dashboard_updated
  belongs_to :project, optional: true, inverse_of: :dashboard
  has_many :home_dashboard_projects, class_name: "Project", foreign_key: "home_dashboard_id",
                                     inverse_of: :home_dashboard, dependent: :nullify
end
