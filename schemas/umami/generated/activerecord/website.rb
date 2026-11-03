class Website < ApplicationRecord
  self.table_name = "website"
  self.primary_key = "website_id"

  attribute :recorder_enabled, default: false

  validates :name, presence: true, length: { maximum: 100 }
  validates :domain, length: { maximum: 500 }

  belongs_to :user, optional: true, inverse_of: :websites
  belongs_to :create_user, class_name: "User", foreign_key: "created_by", optional: true, inverse_of: :created_by
  belongs_to :team, optional: true, inverse_of: :websites
  has_many :annotations, dependent: :restrict_with_error
  has_many :event_data, dependent: :restrict_with_error
  has_many :reports, dependent: :restrict_with_error
  has_many :revenue, class_name: "Revenue", inverse_of: :website, dependent: :restrict_with_error
  has_many :segments, dependent: :restrict_with_error
  has_many :session_data, dependent: :restrict_with_error
  has_many :session_replays, dependent: :restrict_with_error
  has_many :session_replays_saved, class_name: "SessionReplaySaved", inverse_of: :website,
                                   dependent: :restrict_with_error
  has_many :heatmap_events, dependent: :restrict_with_error
end
