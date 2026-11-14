class SessionView < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :is_pinned_by_default_for_project, default: false
  attribute :id, default: -> { Cuid.generate }

  enum :view_mode, { conversational: "conversational", list: "list" }, validate: true
  enum :unmatched_traces, { show: "show", hide: "hide" }, validate: true

  validates :id, uniqueness: { scope: :project_id }
  validates :name, presence: true, uniqueness: { scope: :project_id }
  validates :filters, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :session_views
  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :session_views_created
  has_many :pins, class_name: "SessionViewPinOverride", foreign_key: %i[project_id view_id],
                  primary_key: %i[project_id id], inverse_of: :view, dependent: :destroy
end
