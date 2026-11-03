class SiteSettings < ApplicationRecord
  self.table_name = "SiteSettings"

  attribute :enabled, default: false

  validates :data, exclusion: { in: [ nil ] }

  belongs_to :last_modified_by_user, class_name: "User", foreign_key: "lastModifiedByUserId", optional: true,
                                     inverse_of: :site_settings
end
