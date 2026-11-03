defmodule Teable.AccessToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          user_id: String.t(),
          scopes: String.t(),
          space_ids: String.t() | nil,
          base_ids: String.t() | nil,
          sign: String.t(),
          client_id: String.t() | nil,
          has_full_access: boolean() | nil,
          expired_time: DateTime.t(),
          last_used_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil
        }

  schema "access_token" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:user_id, :string)
    field(:scopes, :string)
    field(:space_ids, :string)
    field(:base_ids, :string)
    field(:sign, :string)
    field(:client_id, :string)
    field(:has_full_access, :boolean)
    field(:expired_time, Teable.PrismaDateTime)
    field(:last_used_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end