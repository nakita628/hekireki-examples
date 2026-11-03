defmodule Postiz.SocialMediaAgencyNiche do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          agency_id: Ecto.UUID.t(),
          niche: String.t(),
          agency: Postiz.SocialMediaAgency.t() | nil
        }

  schema "SocialMediaAgencyNiche" do
    field(:niche, :string, primary_key: true)
    field(:agency_id, :binary_id, primary_key: true, source: :agencyId)
    belongs_to(:agency, Postiz.SocialMediaAgency, foreign_key: :agency_id, define_field: false, type: :binary_id)
  end
end