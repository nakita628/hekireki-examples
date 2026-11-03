class SocialMediaAgencyNiche < ApplicationRecord
  self.table_name = "SocialMediaAgencyNiche"
  self.primary_key = %w[agencyId niche]

  belongs_to :agency, class_name: "SocialMediaAgency", foreign_key: "agencyId", inverse_of: :niches
end
